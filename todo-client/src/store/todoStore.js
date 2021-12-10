import agent from '@/api/agent';

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
      todo.isDone = !todo.isDone;
      todo['priorityId'] = todo.priority.id;

      await agent.ToDos.update(todo.id, todo).catch((error) => {
        console.log(error);
      });
    },
    async sendToArchive(_, id) {
      await agent.ToDos.archive(id).catch((error) => {
        //update will be in websockets
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
