import axios from 'axios';

// axios.defaults.baseURL = 'http://localhost:5125/api';

const todoApiBaseUrl = axios.create({
  baseURL: 'http://localhost:5125/api',
});

const archiveApiBaseUrl = axios.create({
  baseURL: 'http://localhost:5257/api',
});

const responseBody = (response) => response.data;

const todoRequest = {
  get: (a) => todoApiBaseUrl.get(a).then(responseBody),
  post: (url, body) => todoApiBaseUrl.post(url, body).then(responseBody),
  put: (url, body) => todoApiBaseUrl.put(url, body).then(responseBody),
  del: (url) => todoApiBaseUrl.delete(url).then(responseBody),
};

const ToDos = {
  list: () => todoRequest.get('/todos'),
  details: (id) => todoRequest.get(`/todos/${id}`),
  create: (body) => todoRequest.post('/todos', body),
  update: (id, body) => todoRequest.put(`/todos/${id}`, body),
  delete: (id) => todoRequest.del(`/todos/${id}`),
  archive: (id) => todoRequest.post(`/todos/${id}/archive`, id),
};

const archiveRequest = {
  get: (a) => archiveApiBaseUrl.get(a).then(responseBody),
  post: (url, body) => archiveApiBaseUrl.post(url, body).then(responseBody),
};

const Archive = {
  list: () => archiveRequest.get('/todos'),
  details: (id) => archiveRequest.get(`/todos/${id}`),
  unarchive: (id) => archiveRequest.post(`todos/${id}/unarchive`, id),
};

const agent = {
  ToDos,
  Archive,
};

export default agent;
