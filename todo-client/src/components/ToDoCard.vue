<template>
  <va-card class="card">
    <va-card-content>
      <va-tabs v-model="value">
        <template #tabs>
          <va-tab v-for="tab in ['Active', 'Archived']" :key="tab">
            {{ tab }}
          </va-tab>
        </template>
      </va-tabs>
    </va-card-content>
    <transition name="fade" mode="out-in">
      <component :is="getTab()" />
    </transition>
  </va-card>
</template>

<script>
import ToDoList from './ToDoList.vue';
import ArchivedList from './ArchivedList.vue';

import { ref } from 'vue';
export default {
  name: 'ToDoCard',
  components: {
    ToDoList,
    ArchivedList,
  },
  setup() {
    let value = ref(1);

    function getTab() {
      return value.value === 1 ? 'ToDoList' : 'ArchivedList';
    }

    return { value, getTab };
  },
};
</script>

<style scoped>
.card {
  margin: 0 auto;
  width: 400px;
}
</style>
