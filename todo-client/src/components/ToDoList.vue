<template>
  <va-card-content>
    <va-list>
      <va-list-item v-for="todo in todos" :key="todo.id">
        <va-list-item-section>
          <va-list-item-label>
            {{ todo.name }}
          </va-list-item-label>
        </va-list-item-section>
        <va-list-item-section icon>
          <va-icon name="archive" color="gray" />
          <va-icon v-if="todo.done == true" name="done" color="gray" />
          <va-icon
            v-if="todo.done == false"
            name="check_box_outline_blank"
            color="gray"
            @click="toggleDone(todo)"
          />
        </va-list-item-section>
      </va-list-item>
    </va-list>
  </va-card-content>
</template>

<script>
import { useStore } from 'vuex';
export default {
  name: 'ToDoList',
  setup() {
    const store = useStore();

    return {
      todos: store.state.todos,
      toggleDone(todo) {
        store.commit('toggleDone', todo.id);
      },
    };
  },
};
</script>

<style></style>
