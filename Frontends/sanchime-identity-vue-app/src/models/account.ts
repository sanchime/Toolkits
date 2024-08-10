export interface LoginRequest {
    account: string
    password: string
}

export interface LoginResponse {
    token: string
    refreshToken: string
    refreshTokenExpiryTime: Date
}

export interface TokenPayload {
    account: string
    userId: number
    userName: string
    userAvatar: string | null
}