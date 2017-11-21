const REDIRECT_TO = 'REDIRECT_TO';

const initialState = {
  redirectTo: null
};

export const actionCreators = {
  redirectTo: redirectTo => {
    type: REDIRECT_TO, redirectTo;
  }
};

export default function reducer(state = initialState, action = {}) {
  switch (action.type) {
    case REDIRECT_TO:
      return {
        ...state,
        redirectTo: action.redirectTo
      };

    default:
      return state;
  }
}
