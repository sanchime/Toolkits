import { GetMenuTreeQuery, MenuTreeResponse } from '@/models/menu'
import { useGet, usePost } from '@/utils/request'

const getMenuTree = (request: GetMenuTreeQuery): Promise<MenuTreeResponse[]> => {
    return useGet<MenuTreeResponse[]>('/identity/menu/tree', request)
}

const addMenu = (request: GetMenuTreeQuery): Promise<MenuTreeResponse> => {
    return usePost<MenuTreeResponse>('/identity/menu/tree', request)
}

export default {
    getMenuTree,
    addMenu
}