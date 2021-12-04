<template>
  <va-card-content>
    <va-list>
      <transition-group name="fade" tag="div">
        <va-list-item v-for="todo in todos" :key="todo.id">
          <va-list-item-section>
            <va-list-item-label>
              {{ todo.name }}
            </va-list-item-label>
          </va-list-item-section>
          <va-list-item-section icon>
            <va-icon name="reply" color="gray" @click="toggleActive(todo)" />
          </va-list-item-section>
        </va-list-item>
      </transition-group>
    </va-list>
  </va-card-content>
</template>

<script>
import { useStore } from 'vuex';
export default {
  name: 'ArchivedList',
  setup() {
    const store = useStore();

    return {
      todos: store.state.archivedTodos,
      toggleActive(todo) {
        store.commit('toggleActive', todo.id);
      },
    };
  },
};
</script>

<style></style>
