import { State, Actions } from 'jumpstate';
import { Observable } from 'rxjs/Observable';
import { createSelector } from 'reselect';
import { get } from 'redux/client';

export default State('tasks', {
  initial: {
    loading: false,
    loaded: false,
    statusKeys: [],
    statusByKeys: {},
    categoryKeys: [],
    categoryByKeys: {},
    taskByKeys: [],
    notStartedTasks: {},
    inProgressTasks: {},
    pendingTasks: {},
    doneTasks: {}
  },

  loadCategoryTypes(state) {
    return { ...state, loading: true };
  },

  loadCategoryTypesSuccess(state, { categoryTypes }) {
    return {
      ...state,
      loaded: true,
      loading: false,
      categoryKeys: categoryTypes.map(item => {
        return item.key;
      }),
      categoryByKeys: categoryTypes.reduce((obj, cat) => {
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
      categoryByKeys: {}
    };
  },

  loadStatuses(state) {
    return { ...state, loading: true };
  },

  loadStatusesSuccess(state, { statuses }) {
    return {
      ...state,
      loaded: true,
      loading: false,
      statusKeys: statuses.map(item => {
        return item.key;
      }),
      statusByKeys: statuses.reduce((obj, status) => {
        obj[status.key] = status;
        return obj;
      }, {})
    };
  },

  loadStatusesFailed(state, { error }) {
    return {
      ...state,
      error: error,
      loaded: true,
      loading: false,
      statusKeys: [],
      statusByKeys: {}
    };
  },

  loadTasksByStatus(state) {
    return { ...state, loading: true };
  },

  loadTasksByStatusSuccess(state, { tasks }) {
    console.log(tasks);
    return {
      ...state,
      loaded: true,
      loading: false,
      taskByKeys: tasks.reduce((obj, task) => {
        obj[task.id] = task;
        return obj;
      }, {}),
      notStartedTasks: tasks
        .filter(task => task.status === 1)
        .map(task => task.id),
      inProgressTasks: tasks
        .filter(task => task.status === 2)
        .map(task => task.id),
      pendingTasks: tasks
        .filter(task => task.status === 4)
        .map(task => task.id),
      doneTasks: tasks.filter(task => task.status === 8).map(task => task.id)
    };
  },

  loadTasksByStatusFailed(state, { error }) {
    return {
      ...state,
      error: error,
      loaded: true,
      loading: false
    };
  }
});

// selectors
const getTaskState = state => state.task;
export const taskSelectors = {
  getLoading: createSelector(getTaskState, task => task.loading),
  getCategoryByKeys: createSelector(getTaskState, task => task.categoryByKeys),
  getTaskByKeys: createSelector(getTaskState, task => task.taskByKeys),
  getNotStartedTasks: createSelector(
    getTaskState,
    task => task.notStartedTasks
  ),
  getInProgressTasks: createSelector(
    getTaskState,
    task => task.inProgressTasks
  ),
  getPendingTasks: createSelector(getTaskState, task => task.pendingTasks),
  getDoneTasks: createSelector(getTaskState, task => task.doneTasks)
};
export const getCategoryByKeys = createSelector(
  getTaskState,
  task => task.categoryByKeys
);
export const getTaskByKeys = createSelector(
  getTaskState,
  task => task.taskByKeys
);
export const getNotStartedTasks = createSelector(
  getTaskState,
  task => task.notStartedTasks
);
export const getInProgressTasks = createSelector(
  getTaskState,
  task => task.inProgressTasks
);
export const getPendingTasks = createSelector(
  getTaskState,
  task => task.pendingTasks
);
export const getDoneTasks = createSelector(
  getTaskState,
  task => task.doneTasks
);

const taskRequest = {
  loadCategoryTypes: () => get(`task-module/api/tasks/category-types`),
  loadStatuses: () => get(`task-module/api/tasks/task-statuses`),
  loadTasksByStatus: () => get(`task-module/api/tasks/tasks-by-statuses`)
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
            return Actions.tasks.loadCategoryTypesSuccess({
              categoryTypes: result.response
            });
          })
          .catch(error =>
            Observable.of(Actions.tasks.loadCategoryTypesFailed(error))
          )
      );
  },

  action$ => {
    return action$
      .ofType('tasks_loadStatuses')
      .debounceTime(100)
      .switchMap(action =>
        taskRequest
          .loadStatuses()
          .map(result => {
            return Actions.tasks.loadStatusesSuccess({
              statuses: result.response
            });
          })
          .catch(error =>
            Observable.of(Actions.tasks.loadStatusesFailed(error))
          )
      );
  },

  action$ => {
    return action$
      .ofType('tasks_loadTasksByStatus')
      .debounceTime(100)
      .switchMap(action =>
        taskRequest
          .loadTasksByStatus()
          .map(result => {
            return Actions.tasks.loadTasksByStatusSuccess({
              ...result.response
            });
          })
          .catch(error =>
            Observable.of(Actions.tasks.loadTasksByStatusFailed(error))
          )
      );
  }
];
