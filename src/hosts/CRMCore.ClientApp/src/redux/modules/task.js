import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/of';

import { State, Actions } from 'jumpstate';
import { createSelector } from 'reselect';
import { get } from 'redux/client';

export const TaskStatus = {
  NOT_STARTED: 1,
  IN_PROGRESS: 2,
  PENDING: 4,
  DONE: 8
};

export default State('tasks', {
  initial: {
    loading: false,
    loaded: false,
    error: null,
    statusKeys: [],
    statusByKeys: {},
    categoryKeys: [],
    categoryByKeys: {},
    taskByKeys: {},
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
    return {
      ...state,
      loaded: true,
      loading: false,
      taskByKeys: tasks.reduce((obj, task) => {
        obj[task.id] = task;
        return obj;
      }, {}),
      notStartedTasks: tasks
        .filter(task => task.status === TaskStatus.NOT_STARTED)
        .map(task => task.id),
      inProgressTasks: tasks
        .filter(task => task.status === TaskStatus.IN_PROGRESS)
        .map(task => task.id),
      pendingTasks: tasks
        .filter(task => task.status === TaskStatus.PENDING)
        .map(task => task.id),
      doneTasks: tasks
        .filter(task => task.status === TaskStatus.DONE)
        .map(task => task.id)
    };
  },

  loadTasksByStatusFailed(state, { error }) {
    console.log(error);
    return {
      ...state,
      error: error,
      loaded: true,
      loading: false,
      taskByKeys: {},
      notStartedTasks: {},
      inProgressTasks: {},
      pendingTasks: {},
      doneTasks: {}
    };
  }
});

// selectors
const getTaskState = state => state.task;
export const taskSelectors = {
  getLoading: createSelector(getTaskState, task => task.loading),
  getError: createSelector(getTaskState, task => task.error),
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

const taskRequests = {
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
        taskRequests
          .loadCategoryTypes()
          .map(result => {
            return Actions.tasks.loadCategoryTypesSuccess({
              categoryTypes: result.response
            });
          })
          .catch(error =>
            Observable.of(Actions.tasks.loadCategoryTypesFailed({ error }))
          )
      );
  },

  action$ => {
    return action$
      .ofType('tasks_loadStatuses')
      .debounceTime(100)
      .switchMap(action =>
        taskRequests
          .loadStatuses()
          .map(result => {
            return Actions.tasks.loadStatusesSuccess({
              statuses: result.response
            });
          })
          .catch(error =>
            Observable.of(Actions.tasks.loadStatusesFailed({ error }))
          )
      );
  },

  action$ => {
    return action$
      .ofType('tasks_loadTasksByStatus')
      .debounceTime(100)
      .switchMap(action =>
        taskRequests
          .loadTasksByStatus()
          .map(result => {
            return Actions.tasks.loadTasksByStatusSuccess({
              ...result.response
            });
          })
          .catch(error => {
            console.log(error);
            return Observable.of(
              Actions.tasks.loadTasksByStatusFailed({ error })
            );
          })
      );
  }
];
