<template>
  <va-card-content>
    <va-list>
      <transition-group name="fade" tag="div">
        <va-list-item v-for="todo in todos" :key="todo.id">
          <va-list-item-section>
            <va-list-item-label>
              {{ todo.description }}
            </va-list-item-label>
          </va-list-item-section>
          <va-list-item-section icon>
            <va-icon name="reply" color="gray" @click="unarchive(todo)" />
            <va-icon name="clear" color="red" @click="deleteTodo(todo)" />
          </va-list-item-section>
        </va-list-item>
      </transition-group>
    </va-list>
  </va-card-content>
</template>

<script>
import { computed } from '@vue/reactivity';
import { useStore } from 'vuex';
export default {
  name: 'ArchivedList',
  setup() {
    const store = useStore();

    const todos = computed(() => {
      return store.getters['archiveStore/getToDos'];
    });

    return {
      todos,
      unarchive(todo) {
        store.dispatch('archiveStore/unarchive', todo.id);
      },
      deleteTodo(todo) {
        store.dispatch('archiveStore/deleteTodo', todo.id);
      },
    };
  },
};
</script>

<style></style>
