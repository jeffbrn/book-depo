import axios, { AxiosInstance, AxiosResponse } from 'axios';
import BookSumm from '@/common/types/book-summ';
import BookDetails from '@/common/types/book-details';
import RawBookData from '@/common/types/raw-book-data';

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

  async function getRawData(isbn: string): Promise<AxiosResponse<RawBookData>> {
    return http.get(`${url}/${isbn}/raw`);
  }

  return {
    getAll,
    getDetails,
    getRawData,
  };
}

const ApiService = {
  bookApi,
};

export default ApiService;
