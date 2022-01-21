<template>
  <div id="nav">
    <router-link to="/">Home</router-link> |
    <router-link to="/about">About</router-link>
  </div>
  <router-view v-slot="{ Component }">
    <transition name="fade" mode="out-in">
      <component :is="Component" />
    </transition>
  </router-view>
</template>

<script>
import { useStore } from 'vuex';
import * as signalR from '@aspnet/signalr';
import { onMounted } from '@vue/runtime-core';
export default {
  setup() {
    const store = useStore();

    const createdTodo = 'CreatedTodo';
    const updatedTodo = 'UpdatedTodo';
    // const deletedTodo = 'DeletedTodo';
    const archivedTodo = 'ArchivedTodo';
    const todoEvents = ['CreatedTodo', 'UpdatedTodo', 'ArchivedTodo'];
    const archiveEvents = ['UnarchivedTodo', 'DeletedTodo'];

    let todoConnection;
    let archiveConnection;

    onMounted(() => {
      setHubConnections();
      startHubConnections();

      todoEvents.forEach(todoEvent => {
        subscribeToTodoEvents(todoEvent);
      });

      archiveEvents.forEach(todoEvent => {
          subscribeToArchiveEvents(todoEvent);
      });
    });

    const setHubConnections = () => {
      todoConnection = new signalR.HubConnectionBuilder()
        .withUrl('http://localhost:5125/todoHub')
        .configureLogging(signalR.LogLevel.Information)
        .build();

      archiveConnection = new signalR.HubConnectionBuilder()
      .withUrl('http://localhost:5257/archiveHub')
      .configureLogging(signalR.LogLevel.Information)
      .build();
    };

    const startHubConnections = () => {
      todoConnection
        .start()
        .catch(function (err) {
          return console.error(err.toString());
      });

      archiveConnection
        .start()
        .catch(function (err) {
          return console.error(err.toString());
      });
    };

    const subscribeToTodoEvents = (eventName) => {
      todoConnection.on(eventName, (message) => {
        console.log('a venit eventu din Todo:', eventName, message);

        if (eventName == createdTodo)
          store.dispatch('todoStore/onAddTodoEvent', message);
        if (eventName == updatedTodo)
          store.dispatch('todoStore/onUpdateTodoEvent', message);
        if (eventName == archivedTodo)
          store.dispatch('todoStore/onArchiveTodoEvent', message);
      });
    };

    const subscribeToArchiveEvents = (eventName) => {
      archiveConnection.on(eventName, (message) => {
        console.log('a venit eventu din archive:', eventName, message);
      });
    };
  },

};
</script>

<style>
body {
  background-image: linear-gradient(to right, #6e44ff 0%, #b892ff 100%);
}
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #151515;
}

.fade-enter,
.fade-leave-to {
  opacity: 0;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-to,
.fade-leave-from {
  opacity: 1;
}
</style>
