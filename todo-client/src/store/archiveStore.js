export default {
  state: {
    archivedTodos: null,
  },
  mutations: {
    saveTodos(state, todos) {
      state.archivedTodos = todos;
    },
  },
  actions: {},
  getters: {
    getArchived(state) {
      return state.archivedTodos;
    },
  },
};
