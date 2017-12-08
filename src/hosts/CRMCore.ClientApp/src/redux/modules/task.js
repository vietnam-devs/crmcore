import { State } from 'jumpstate';

export default State('tasks', {
  initial: {
    loading: false
  },

  showLoading(state) {
    return { loading: true, ...state };
  }
});
