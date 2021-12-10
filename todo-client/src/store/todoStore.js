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
    addTodo(state, todo) {
      state.todos.push(todo);
    },
    updateTodo(state, todo) {
      const index = state.todos.findIndex((t) => t.id === todo.id);
      state.todos.splice(index, 1, todo);
    },
    deleteTodo(state, todo) {
      const index = state.todos.findIndex((t) => t.id === todo.id);
      state.todos.splice(index, 1);
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
        console.log(error);
      });
    },
    onAddTodoEvent({ commit }, todo) {
      commit('addTodo', todo);
    },
    onUpdateTodoEvent({ commit }, todo) {
      commit('updateTodo', todo);
    },
    onDeleteTodoEvent({ commit }, todo) {
      commit('deleteTodo', todo);
    },
    onArchiveTodoEvent({ commit }, todo) {
      console.log('nu merge ba', todo);
      commit('deleteTodo', todo);
    },
  },
  getters: {
    getToDos(state) {
      return state.todos;
    },
  },
};
