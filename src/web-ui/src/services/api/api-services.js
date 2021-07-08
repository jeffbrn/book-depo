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

  async function getStats() {
    return http.get(`${url}/stats`);
  }

  return {
    getAll,
    getDetails,
    getStats
  };
}

const ApiService = {
  bookApi,
};

export default ApiService;
