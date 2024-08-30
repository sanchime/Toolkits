import { useGet, usePost, usePut, useDelete } from '@/utils/request'
import { RoleResponse, AddRoleRequest, UpdateRoleRequest, UpdateRolePermissionRequest } from "@/models/role"

const getRoles = () => {
    return useGet<RoleResponse[]>("/identity/role")
}

const addRole = (request: AddRoleRequest) => {
    return usePost("/identity/role", request)
}

const updateRole = (id: number, request: UpdateRoleRequest) => {
    return usePut(`/identity/role/${id}`, request);
}

const deleteRole = (id: number) => {
    return useDelete(`/identity/role/${id}`)
}

const updateRolePermissions = (id: number, request: UpdateRolePermissionRequest) => {
    return usePost(`/identity/role/${id}/permissions`, request)
}

export default {
    getRoles,
    addRole,
    updateRole,
    deleteRole,
    updateRolePermissions
}