import { useGet, usePost, usePut, useDelete } from '@/utils/request'
import { UserResponse } from "@/models/user"
import { PaginatedResult } from "@/models"


const getUsers = (pageIndex: number  = 1, pageSize: number = 20) => {
    return useGet<PaginatedResult<UserResponse>>(`/identity/user?PageIndex=${pageIndex}&PageSize=${pageSize}`)
}


export default {
    getUsers
}