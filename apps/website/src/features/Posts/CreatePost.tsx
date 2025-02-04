import { useState } from "react";
import { useSession } from "../../hooks/useSession";
import { CreatePostRequest } from "./Posts.types";
import { useMutation, useQuery } from "@tanstack/react-query";
import { getAllSubjects } from "../../endpoints/subjectEndpoints";
import { createPost } from "../../endpoints/postEndpoints";
import { MultiValue } from "react-select";
import { SubjectDto } from "../Subjects/Subjects.types";
import { UniCollabForm } from "../../components/Form/UniCollabForm";
import InputText from "../../components/Form/InputText";
import InputTextArea from "../../components/Form/InputTextArea";
import Select from "react-select";
import { SubmitButton } from "../../components/Button";
import WaitForQuery from "../../components/WaitForQuery";

const initialState: CreatePostRequest = {
    title: '',
    content: '',
    subjects: []
}

export default function CreatePost() {
    const {user} = useSession()
    const [request, setRequest] = useState<CreatePostRequest>(initialState)

    const subjectQuery = useQuery({
        queryKey: ['subjects'],
        queryFn: getAllSubjects
    })

    const mutation = useMutation({
        mutationFn: (request: CreatePostRequest) => createPost(user.id, request)
    })

    const handleSubmit = async () => {
        if (!subjectQuery.isSuccess)
            return;

        await mutation.mutateAsync(request)
        setRequest(initialState)
    }

    const setTitle = (title: string) => setRequest(req => ({...req, title}))
    const setContent = (content: string) => setRequest(req => ({...req, content}))
    const setSubjects = (subjects: MultiValue<SubjectDto>) => setRequest(req => ({
        ...req,
        subjects: subjects.map(s => s.id) 
    }))

    return (
        <UniCollabForm name="create-post" onSubmit={handleSubmit}>
            <div className="p-3 border d-flex flex-column rounded">
                <h2>Create Post</h2>

                <InputText onChange={setTitle} value={request.title} label="Title"/>
                <InputTextArea onChange={setContent} value={request.content} label="Content"/>
                
                <label className="form-label">Subjects</label>
                <WaitForQuery query={subjectQuery}>
                    <Select 
                        isMulti
                        defaultValue={new Array<SubjectDto>()}
                        getOptionLabel={s => s.name}
                        getOptionValue={s => s.id}
                        onChange={setSubjects}
                        options={subjectQuery.data} />
                </WaitForQuery>

                <SubmitButton 
                    color={'primary'}
                    className="mt-3 align-self-center"
                    loadingText={`Uploading ${request.title}...`}>
                    Create
                </SubmitButton>
            </div>
        </UniCollabForm>
    )
}