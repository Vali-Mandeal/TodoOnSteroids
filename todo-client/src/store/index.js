import { createStore } from 'vuex';
import agent from '@/api/agent';

export default createStore({
  state: {
    todos: [
      { id: 1, name: 'Learn JavaScript', done: true },
      { id: 2, name: 'Learn Vue', done: false },
      { id: 3, name: 'Build something awesome', done: false },
    ],
    archivedTodos: [
      { id: 4, name: 'Go to the gym', done: true },
      { id: 5, name: 'Learn Python', done: true },
      { id: 6, name: 'Learn React', done: true },
    ],
  },
  mutations: {
    toggleDone(state, id) {
      const todo = state.todos.find((todo) => todo.id === id);
      todo.done = !todo.done;
    },
    toggleActive(state, id) {
      const todo = state.archivedTodos.find((todo) => todo.id === id);
      state.todos.push(todo);
      state.archivedTodos.splice(state.archivedTodos.indexOf(todo), 1);
    },
    sendToArchive(state, id) {
      const todo = state.todos.find((todo) => todo.id === id);
      state.archivedTodos.push(todo);
      state.todos.splice(state.todos.indexOf(todo), 1);
    },
  },
  actions: {
    async fetchToDos() {
      await agent.ToDos.list()
        .then((response) => {
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  modules: {},
});