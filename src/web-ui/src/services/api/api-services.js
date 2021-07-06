import axios from 'axios';

const http = axios.create();

const baseUrl = 'https://localhost:7001/api';

function bookApi() {
  const url = `${baseUrl}/book`;

  async function getAll() {
    return http.get(`${url}`);
  }

  async function getDetails(bid) {
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
