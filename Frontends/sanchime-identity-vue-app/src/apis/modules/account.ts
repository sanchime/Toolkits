import { LoginRequest, LoginResponse } from '../../models/account'
import { usePost } from '../../utils/request'

const login = (request: LoginRequest): Promise<LoginResponse> => {
    return usePost<LoginResponse>('/identity/account/token', request)
}

export default {
    login
}