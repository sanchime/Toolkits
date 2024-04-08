import { defineStore } from 'pinia'
import { LoginResponse, TokenPayload } from '../models/account'
import { reactive } from 'vue'

export const useTokenStore = defineStore('token', () => {
    const state = reactive<LoginResponse>({ token: "", refreshToken: "", refreshTokenExpiryTime: new Date() })

    const user = reactive<TokenPayload>({ account: "", userId: 0, userName: "" })

    const setToken = (info: LoginResponse) => {
        if (!!info) {
            state.token = info.token
            state.refreshToken = info.refreshToken
            state.refreshTokenExpiryTime = info.refreshTokenExpiryTime

            const parts = info.token.split(".");
            const payload = JSON.parse(atob(parts[1]));

            user.account = payload.account
            user.userId = payload.userId as number
            user.userName = payload.userName
        }
        return info
    }

    return { state, user, setToken }
}, 
{
    persist: true
})