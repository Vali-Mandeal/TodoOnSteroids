import agent from '@/api/agent';

export default {
  namespaced: true,
  state: {
    todos: new Array(),
  },
  mutations: {
    saveTodos(state, todos) {
      state.todos = todos;
    },
    addTodo(state, todo) {
      state.todos.push(todo);
    },
    updateTodo(state, todo) {
      state.todos = state.todos.map((t) => (t.id === todo.id ? todo : t));
    },
    deleteTodo(state, todo) {
      if (state.todos.length == 1) {
        state.todos = new Array();
      } else {
        state.todos = state.todos.filter((t) => t.id !== todo.id);
      }
    },
  },
  actions: {
    async fetchToDos({ commit }) {
      await agent.ToDos.list()
        .then((response) => {
          if (response.length > 0) {
            commit('saveTodos', response);
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },
    async toggleStatus(_, todo) {
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
      console.log('onArchiveTodoEvent', todo);
      commit('deleteTodo', todo);
    },
  },
  getters: {
    getToDos(state) {
      return state.todos;
    },
  },
};
