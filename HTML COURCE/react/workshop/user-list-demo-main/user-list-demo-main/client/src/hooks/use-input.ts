import { useReducer, useCallback, useEffect, FormEvent } from 'react';

import { IValidationFn } from '../utils/validations';
import { initialState, reducer } from './input-reducer';

const useInput = (validateValue: IValidationFn, defaultValue?: string | undefined) => {
  const [state, dispatch] = useReducer(reducer, initialState);

  useEffect(() => {
    let timer = setTimeout(() => {
      if (state.touched) {
        const { isValid, message } = validateValue(state.value);
        dispatch({
          type: 'error',
          error: !isValid && state.touched,
          errorMessage: message,
          isValid,
        });
      }
    }, 300);

    return () => { clearTimeout(timer); }
  }, [validateValue, state.touched, state.value]);

  useEffect(() => { defaultValue && dispatch({ type: 'set_value', value: defaultValue }); }, [defaultValue]);

  const changeHandler = useCallback(
    ({ currentTarget: { value } }: FormEvent<HTMLInputElement | HTMLTextAreaElement>) => {
      dispatch({ type: 'change', value });
    },
    []
  );

  const blurHandler = useCallback(() => { dispatch({ type: 'blur' }); }, []);

  const resetHandler = useCallback(() => { dispatch({ type: 'reset' }); }, []);

  return {
    value: state.value,
    isValid: state.isValid,
    hasError: state.hasError,
    errorMessage: state.errorMessage,
    changeHandler,
    blurHandler,
    resetHandler,
    touched: state.touched,
  };
};

export default useInput;
