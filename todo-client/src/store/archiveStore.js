import agent from '@/api/agent';

export default {
  namespaced: true,
  state: {
    archivedTodos: null,
  },
  mutations: {
    saveTodos(state, todos) {
      state.archivedTodos = todos;
    },
  },
  actions: {
    async fetchToDos({ commit }) {
      await agent.Archive.list()
        .then((response) => {
          commit('saveTodos', response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    async unarchive(_, id) {
      await agent.Archive.unarchive(id).catch((error) => {
        console.log(error);
      });
    },
  },
  getters: {
    getToDos(state) {
      return state.archivedTodos;
    },
  },
};
