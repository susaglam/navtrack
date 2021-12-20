// custom-instance.ts

import Axios, { AxiosRequestConfig } from "axios";

export const AXIOS_INSTANCE = Axios.create({ baseURL: process.env.REACT_APP_API_URL });

export const axiosInstance = <T>(config: AxiosRequestConfig): Promise<T> => {
  const source = Axios.CancelToken.source();

  const promise = AXIOS_INSTANCE({ ...config, cancelToken: source.token }).then(({ data }) => data);

  // @ts-ignore

  promise.cancel = () => {
    source.cancel("Query was cancelled by React Query");
  };

  return promise;
};