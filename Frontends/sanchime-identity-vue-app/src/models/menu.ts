export interface MenuTreeResponse {
    id: number
    code: string
    name: string
    icon: string
    path: string
    route: string
    order: number
    parentId: number,
    children: MenuTreeResponse[]
}