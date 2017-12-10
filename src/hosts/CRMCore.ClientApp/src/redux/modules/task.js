import { State, Actions } from 'jumpstate';
import { Observable } from 'rxjs/Observable';
import { get } from 'redux/client';

export default State('tasks', {
  initial: {
    loading: false,
    loaded: false,
    statusKeys: [],
    taskKeys: [],
    categoryKeys: [],
    categoriesByKey: {},
    statusByKey: {},
    tasksByStatus: {}
  },

  loadCategoryTypes(state) {
    return { ...state, loading: true };
  },

  loadCategoryTypesSuccessed(state, { categoryTypes }) {
    return {
      ...state,
      loaded: true,
      loading: false,
      categoryKeys: categoryTypes.map(item => {
        return item.key;
      }),
      categoriesByKey: categoryTypes.reduce((obj, cat) => {
        obj[cat.key] = cat;
        return obj;
      }, {})
    };
  },

  loadCategoryTypesFailed(state, { error }) {
    return {
      ...state,
      error: error,
      loaded: true,
      loading: false,
      categoryKeys: [],
      categoriesByKey: {}
    };
  }
});

const taskRequest = {
  loadCategoryTypes: () => get(`task-module/api/tasks/category-types`)
};

export const taskEpics = [
  action$ => {
    return action$
      .ofType('tasks_loadCategoryTypes')
      .debounceTime(100)
      .switchMap(action =>
        taskRequest
          .loadCategoryTypes()
          .map(result => {
            return Actions.tasks.loadCategoryTypesSuccessed({
              categoryTypes: result.response
            });
          })
          .catch(error =>
            Observable.of(Actions.tasks.loadCategoryTypesFailed(error))
          )
      );
  }
];
