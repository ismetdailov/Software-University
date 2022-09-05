type State = typeof initialState;
type Action =
  | { type: 'set_value'; value: string }
  | { type: 'change'; value: string }
  | { type: 'blur' }
  | { type: 'error'; error: boolean; errorMessage: string; isValid: boolean }
  | { type: 'reset' };

export const initialState = {
  value: '',
  isValid: false,
  touched: false,
  errorMessage: '',
  hasError: false,
};

export const reducer = (state: State, action: Action) => {
  switch (action.type) {
    case 'set_value': return { ...state, value: action.value, isValid: true };
    case 'change': return { ...state, value: action.value, touched: true };
    case 'blur': return { ...state, touched: true };
    case 'error': return {
      ...state, hasError: action.error, errorMessage: action.errorMessage, isValid: action.isValid
    };
    case 'reset': return initialState;
    default: return state;
  }
};
