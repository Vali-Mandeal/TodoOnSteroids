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
            <va-icon name="reply" color="gray" @click="toggleActive(todo)" />
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
    if (todos.value == null) {
      store.dispatch('archiveStore/fetchToDos');
    }

    return {
      todos,
      toggleActive(todo) {
        store.commit('toggleActive', todo.id);
      },
    };
  },
};
</script>

<style></style>
