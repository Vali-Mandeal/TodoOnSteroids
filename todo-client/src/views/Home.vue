<template>
  <div class="home">
    <ToDoCard msg="Welcome to Your Vue.js App" />
  </div>
</template>

<script>
import ToDoCard from '@/components/ToDoCard.vue';

export default {
  name: 'Home',
  components: {
    ToDoCard,
  },
};
</script>
