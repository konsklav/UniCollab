import React, { useMemo } from "react";
import { Button } from "../../components/Button";
import { RichUserInformation } from "../Users/Users.types";

interface UserCardProps {
    info: RichUserInformation
    onAddFriend: (friendId: string) => Promise<void>
    onRemoveFriend: (unfriendId: string) => Promise<void>
}

import styles from './UserCard.module.css'
import { generateUniqueId } from "../../utils/uniqueId";

const list = styles['user-card-list']

export default function UserCard({info, onAddFriend, onRemoveFriend}: UserCardProps) { 
    return (
        <div className="card">
            <div className="card-header">
                <h2 className="fw-bold">{info.user.username}</h2>
            </div>
            <div className="card-body d-flex flex-column gap-2">
                <Section 
                    header={<>Mutual Friends: {info.mutualFriends.length}</>}
                    collapse={<>{info.mutualFriends.map(user => (
                        <li key={user.id}>{user.username}</li>
                    ))}</>}/>

                <Section 
                    header={<>Mutual Chats: {info.mutualChats.length}</>}
                    collapse={<>{info.mutualChats.map(chat => (
                        <li key={chat.id}>{chat.name}</li>
                    ))}</>}/>
                
                <Section 
                    header={<>Total Posts: {info.totalPostsUploaded}</>}
                    collapse={<>{info.postsPerSubject.map(subject => (
                        <li key={subject.name}>
                            <strong>{subject.name}</strong>: {subject.count}
                        </li>
                    ))}</>}/>
                
                <div className="mt-3">
                    {info.isFriend
                        ? <Button color={'danger'} onClick={() => onRemoveFriend(info.user.id)}>Remove Friend</Button>
                        : <Button color={'success'} onClick={() => onAddFriend(info.user.id)}>Add Friend</Button>
                    }
                </div>
            </div>
        </div>
    )
}

type SectionProps = {header: React.ReactNode, collapse: React.ReactNode}
function Section({header, collapse}: SectionProps) {
    const id = useMemo(() => generateUniqueId(), [])

    return (
        <section>
            <div className="d-flex gap-1 align-items-center">
                <button type="button" className="btn" data-bs-toggle="collapse" data-bs-target={`#${id}`}>
                    +
                </button>
                <h4>{header}</h4>
            </div>
            <ul className={`${list} collapse`} id={id}>
                {collapse}
            </ul>
        </section>
    )
}