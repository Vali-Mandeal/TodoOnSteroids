<template>
  <div class="home">
    <ToDoCard />
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
