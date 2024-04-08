import { stringifyQuery, LocationQueryRaw } from 'vue-router'
import { computed, unref } from 'vue'
import { createFetch, isObject, MaybeRef, UseFetchReturn } from '@vueuse/core'
import { error } from './notification'
import { da } from 'element-plus/es/locale';

const notifyError = error;

const baseUrl = import.meta.env.VITE_BASE_URL
const useFetch = createFetch({
    baseUrl,
    options: {
        immediate: false,
        timeout: 30000,
        beforeFetch({ options, cancel }) {
            const token = ''
            if (!!token) {
                options.headers = Object.assign(options.headers || {}, {
                    ['Authorization']: token,
                })
    
            }
            return { options }
        },
        afterFetch({ data, response }) {
            return { data, response }
        },
        onFetchError({ data, error }) {
            notifyError(data.message);
            data = undefined
            return { data, error }
        },
    },
    fetchOptions: {
        mode: 'cors',
        // credentials: 'include',
    },
})

export async function useGet<T = unknown>(
    url: MaybeRef<string>,
    query?: MaybeRef<unknown>,
): Promise<T> {
    const _url = computed(() => {
        const _url = unref(url)
        const _query = unref(query)
        const queryString = isObject(_query)
            ? stringifyQuery(_query as LocationQueryRaw)
            : _query || ''
        return `${_url}${queryString ? '?' : ''}${queryString}`
    })

    const { data, execute } = useFetch<T>(_url).json()

    await execute()

    return data.value
}

export async function usePost<T = unknown>(
    url: MaybeRef<string>,
    payload?: MaybeRef<unknown>,
): Promise<T> {
    const { data, execute } =  useFetch<T>(url).post(payload).json()

    await execute()

    return data.value
}

export async function usePut<T = unknown>(
    url: MaybeRef<string>,
    payload?: MaybeRef<unknown>,
): Promise<T> {
    const { data, execute } =  useFetch<T>(url).put(payload).json()

    await execute()

    return data.value
}

export async function useDelete<T = unknown>(
    url: MaybeRef<string>,
    query?: MaybeRef<unknown>,
): Promise<T> {
    const _url = computed(() => {
        const _url = unref(url)
        const _query = unref(query)
        const queryString = isObject(_query)
            ? stringifyQuery(_query as LocationQueryRaw)
            : _query || ''
        return `${_url}${queryString ? '?' : ''}${queryString}`
    })

    const { data, execute } =  useFetch<T>(_url).delete().json()

    await execute()

    return data.value
}