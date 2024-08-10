import { RoleResponse } from "./role"

export interface UserResponse {
    id: number
    name: string
    phone: string | null
    email: string | null
    avatar: string | null
    isEnabled: boolean
}

export interface UserRolesResponse {
    id: number
    name: string
    roles: RoleResponse[]
}