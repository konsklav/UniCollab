import { SubjectDto } from "../features/Subjects/Subjects.types"
import { api } from "../services/apiService"

export const getAllSubjects = async (): Promise<readonly SubjectDto[]> => {
    const response = await api.get('/subjects')
    return response.data
}