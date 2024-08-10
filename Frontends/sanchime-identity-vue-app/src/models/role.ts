export interface RoleResponse {
    id: number
    code: string
    name: string
    description: string | null
    isEnabled: boolean
}

export interface AddRoleRequest {
    code: string
    name: string
    description: string | null
}

export interface UpdateRoleRequest {
    code: string
    name: string
    description: string | null
    isEnabled: boolean
}

export interface UpdateRolePermissionRequest {
    permissions: number[]
}