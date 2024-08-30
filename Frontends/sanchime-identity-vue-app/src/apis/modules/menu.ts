import { MenuTreeResponse } from '@/models/menu'
import { useGet, usePost } from '@/utils/request'

const getMenuTree = (): Promise<MenuTreeResponse[]> => {
    return useGet<MenuTreeResponse[]>('/identity/menu/tree')
}

const addMenu = (request: any): Promise<MenuTreeResponse> => {
    return usePost<MenuTreeResponse>('/identity/menu/tree', request)
}

export default {
    getMenuTree,
    addMenu
}