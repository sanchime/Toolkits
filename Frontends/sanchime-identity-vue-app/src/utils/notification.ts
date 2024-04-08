import { ElNotification } from 'element-plus'

const notification = (title: string | undefined, message: string, type: string) => {
    ElNotification({
        title:  title,
        message: message,
        type: type,
    })
}

export const success = (message: string, title: string | undefined = "成功") => {
    notification(title, message, 'success')
}

export const error = (message: string,  title: string | undefined = "错误") => {
    notification(title, message, 'error')
}

export const warning = (message: string, title: string | undefined = "警告") => {
    notification(title, message, 'warning')
}

export const info = (message: string, title: string | undefined = "提示") => {
    notification(title, message, 'info')
}