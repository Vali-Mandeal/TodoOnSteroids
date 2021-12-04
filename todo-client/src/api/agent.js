import axios from 'axios';

axios.defaults.baseURL = 'http://localhost:5125/api';

const responseBody = (response) => response.data;

const requests = {
  get: (url) => axios.get(url).then(responseBody),
  post: (url, body) => axios.post(url, body).then(responseBody),
  put: (url, body) => axios.put(url, body).then(responseBody),
  del: (url) => axios.delete(url).then(responseBody),
};

const ToDos = {
  list: () => requests.get('/todos'),
  details: (id) => requests.get(`/todos/${id}`),
  create: (body) => requests.post('/todos', body),
  update: (id, body) => requests.put(`/todos/${id}`, body),
  delete: (id) => requests.del(`/todos/${id}`),
};

const agent = {
  ToDos,
};

export default agent;
