import { useGet, usePost, usePut, useDelete } from '@/utils/request'
import { RoleResponse, AddRoleRequest, UpdateRoleRequest, UpdateRolePermissionRequest } from "@/models/role"

const getRoles = () => {
    return useGet<RoleResponse[]>("/api/v1/identity/role")
}

const addRole = (request: AddRoleRequest) => {
    return usePost("/api/v1/identity/role", request)
}

const updateRole = (id: number, request: UpdateRoleRequest) => {
    return usePut(`/api/v1/identity/role/${id}`, request);
}

const deleteRole = (id: number) => {
    return useDelete(`/api/v1/identity/role/${id}`)
}

const updateRolePermissions = (id: number, request: UpdateRolePermissionRequest) => {
    return usePost(`/api/v1/identity/role/${id}/permissions`, request)
}

export {
    getRoles,
    addRole,
    updateRole,
    deleteRole,
    updateRolePermissions
}