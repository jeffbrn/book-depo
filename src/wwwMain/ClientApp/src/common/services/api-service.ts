import axios, { AxiosInstance, AxiosResponse } from 'axios';
import BookSumm from '@/common/types/booksumm';

const http: AxiosInstance = axios.create();

const baseUrl = '/api';

function bookApi() {
  const url = `${baseUrl}/book`;

  async function getAll(): Promise<AxiosResponse<BookSumm[]>> {
    return http.get(`${url}`);
  }

  return {
    getAll,
  };
}

const ApiService = {
  bookApi,
};

export default ApiService;
