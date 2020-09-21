import axios, { AxiosInstance, AxiosResponse } from 'axios';
import BookSumm from '@/common/types/booksumm';
import BookDetails from '@/common/types/bookdetails';

const http: AxiosInstance = axios.create();

const baseUrl = '/api';

function bookApi() {
  const url = `${baseUrl}/book`;

  async function getAll(): Promise<AxiosResponse<BookSumm[]>> {
    return http.get(`${url}`);
  }

  async function getDetails(bid: string): Promise<AxiosResponse<BookDetails>> {
    return http.get(`${url}/${bid}`);
  }

  return {
    getAll,
    getDetails,
  };
}

const ApiService = {
  bookApi,
};

export default ApiService;
