import agent from '@/api/agent';

export default {
  namespaced: true,
  state: {
    archivedTodos: new Array(),
  },
  mutations: {
    saveTodos(state, todos) {
      state.archivedTodos = todos;
    },
    addTodo(state, todo) {
      state.archivedTodos.push(todo);
    },
    deleteTodo(state, todoId) {
      if (state.archivedTodos.length == 1) {
        state.archivedTodos = new Array();
      } else {
        state.archivedTodos = state.archivedTodos.filter(
          (t) => t.id !== todoId
        );
      }
    },
  },
  actions: {
    async fetchToDos({ commit }) {
      await agent.Archive.list()
        .then((response) => {
          if (response.length > 0) {
            commit('saveTodos', response);
          }
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
    async deleteTodo(_, id) {
      await agent.Archive.delete(id).catch((error) => {
        console.log(error);
      });
    },
    onUnarchive({ commit }, todo) {
      commit('deleteTodo', todo.id);
    },
    onAddTodo({ commit }, todo) {
      console.log('onAddTodo', todo);
      commit('addTodo', todo);
    },
  },
  getters: {
    getToDos(state) {
      return state.archivedTodos;
    },
  },
};
