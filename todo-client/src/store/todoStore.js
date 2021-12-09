import agent from '@/api/todosAgent';

export default {
  namespaced: true,
  state: {
    todos: null,
  },
  mutations: {
    saveTodos(state, todos) {
      state.todos = todos;
    },
  },
  actions: {
    async fetchToDos({ commit }) {
      await agent.ToDos.list()
        .then((response) => {
          commit('saveTodos', response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    async toggleDone(_, todo) {
      //not implemented
      await agent.ToDos.update(todo.id, todo).catch((error) => {
        console.log(error);
      });
    },
    async sendToArchive(_, id) {
      await agent.ToDos.archive(id).catch((error) => {
        console.log(error);
      });
    },
  },
  getters: {
    getToDos(state) {
      return state.todos;
    },
  },
};
