import { RouteRecordRaw } from 'vue-router'
import { GetMenuTreeQuery, MenuTreeResponse } from '@/models/menu'
import menu from '@/apis/modules/menu'
import { MenuType } from '@/enums/menu'

export const getRouters = async () : Promise<RouteRecordRaw[]> => {
    const menuList = await menu.getMenuTree({ type: MenuType.Page} as GetMenuTreeQuery)
    const loop = (menus: MenuTreeResponse[]): RouteRecordRaw[] => {
        return menus.map((menu) => {
            const route: RouteRecordRaw = {
                path: menu.route,
                name: menu.name,
                component: () => import(`@/views${menu.path}`),
                meta: { 
                    hidden: false,
                    icon: menu.icon,
                    id: menu.id,
                    code: menu.code,
                },
            }
            if (menu.children && menu.children.length > 0) {
                route.children = loop(menu.children)
            }
            return route
        })
    }

    return loop(menuList);
}