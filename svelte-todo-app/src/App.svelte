<script lang="ts">

  import { slide } from "svelte/transition";
  import { elasticInOut } from "svelte/easing";
  import { onMount } from "svelte";

  let todos: any[] = [];
  let input = "";

  onMount(async () => {
    await fetch('https://localhost:5001/Todo/get-all-todos')
      .then(r => r.json())
      .then(data => {
        todos = data;
      });
  })

  async function addTodo() {
    if (input) {
      todos = [
        ...todos,
        {
          description: input
        }
      ];
    }

    const res = await fetch('https://localhost:5001/Todo/create-todo', {
			method: 'POST',
			body: JSON.stringify({
				description: input,
        status: true
			}),
      headers: {
					'Content-Type': 'application/json'
				}
		})

    input = "";
  }

  async function removeTodo(id: any) {
    const index = todos.findIndex(todo => todo.id === id);
    todos.splice(index, 1);
    todos = todos;

    const res = await fetch('https://localhost:5001/Todo/delete-todo?todoId='+id, {
			method: 'DELETE',
      headers: {
					'Content-Type': 'application/json'
				}
		})

  }

  async function editTodo(id: any) {
    const index = todos.findIndex(todo => todo.id === id);
    todos[index].description = input

    const res = await fetch('https://localhost:5001/Todo/update-todo', {
			method: 'PUT',
			body: JSON.stringify({
        id: todos[index].id,
				description: input,
        status: true
			}),
      headers: {
					'Content-Type': 'application/json'
				}
		})
  }

</script>

<svelte:head>
  <link
    rel="stylesheet"
    type="text/css"
    href="https://cdn.jsdelivr.net/npm/bulma@0.8.0/css/bulma.min.css"
  />
  <script src="https://use.fontawesome.com/releases/v5.3.1/js/all.js"></script>
</svelte:head>

<main>
  <div class="columns is-centered is-vcentered is-mobile">
    <div class="column is-narrow" style="width: 70%">
      <h1 class="has-text-centered title">Svelte TODO</h1>
      <form  class="field has-addons"  style="justify-content: center"  on:submit|preventDefault="{addTodo}">

        <div class="control">
          <input bind:value="{input}" class="input" type="text" placeholder="TODO" />
        </div>
  
        <div class="control">
          <button class="button is-primary">
            <span class="icon is-small">
              <i class="fas fa-plus"></i>
            </span>
          </button>
        </div>
        
      </form>

      <ul class:list={todos.length > 0}>
      {#if todos}
        {#each todos as todo (todo.id)}
          <li class="list-item" transition:slide="{{duration: 300, easing: elasticInOut}}">
            <div class="is-flex" style="align-items: center">
              <span class="is-pulled-left">{todo.description}</span>
              <div style="flex: 1"></div>

              <button class="button is-text is-pulled-right is-small" on:click={()=> removeTodo(todo.id)}>
                <span class="icon">
                  <i class="fas fa-check"></i>
                </span>
              </button>
              
              <button class="button is-text is-pulled-right is-small" on:click={()=> editTodo(todo.id)}>
                <span class="icon">
                  <i class="fa fa-pencil"></i>
                </span>
              </button>

            </div>
          </li>
        {:else}
          <li class="has-text-centered" transition:slide="{{delay: 600, duration: 300, easing: elasticInOut}}">
            Nothing here!
          </li>
        {/each}
      {/if}
      </ul>
    </div>
  </div>
</main>

<style>

</style>
