import { MenuType } from '@/enums/menu'

export interface MenuTreeResponse {
    id: number
    code: string
    name: string
    icon: string
    path: string
    route: string
    children: MenuTreeResponse[]
}

export interface GetMenuTreeQuery {
    type: MenuType
}