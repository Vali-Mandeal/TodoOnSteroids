import { createStore } from 'vuex';
import todoStore from './todoStore';
import archiveStore from './archiveStore';

export default createStore({
  state: {},
  mutations: {},
  actions: {},
  getters: {},
  modules: { todoStore, archiveStore },
});
