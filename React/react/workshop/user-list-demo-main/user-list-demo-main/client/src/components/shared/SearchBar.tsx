import { FC, FormEvent, useCallback, useEffect, useState } from 'react';
import { useSearchParams } from 'react-router-dom';

import { faClose, faSearch, IconDefinition } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import Button from './Button';
import styles from './SearchBar.module.css';

const SearchBar: FC<{
  title: string;
  icon: IconDefinition;
  criterion: { value: string; text: string }[];
}> = ({ title, icon, criterion }) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const [state, setState] = useState({ search: '', criteria: '' });

  const changeHandler = useCallback(
    (type: 'search' | 'criteria', { currentTarget: { value } }: FormEvent<HTMLSelectElement | HTMLInputElement>) => {
      setState((state) => ({ ...state, [type]: value }));
    },
    []
  );

  const onClear = useCallback(() => setState((state) => ({ ...state, search: '' })), []);

  useEffect(() => {
    let timer = setTimeout(() => {
      if (state.criteria) {
        searchParams.set('search', state.search);
        searchParams.set('criteria', state.criteria);
        setSearchParams(searchParams);
      }
    }, 300);
    return () => clearTimeout(timer);
  }, [state, searchParams, setSearchParams]);

  return (
    <form className={styles['search-form']}>
      <h2>
        <FontAwesomeIcon icon={icon} className={styles.icon} />
        <span>{title}</span>
      </h2>
      <div className={styles['search-input-container']}>
        <input
          type='text'
          placeholder={state.criteria ? `Search ${title} by ${state.criteria}` : 'Please, select the search criteria'}
          name='search'
          onChange={changeHandler.bind(null, 'search')}
          value={state.search}
        />
        {state.search !== '' && <Button icon={faClose} classes={`${styles['btn']} ${styles['close-btn']}`} onClick={onClear} />}

        <Button
          icon={faSearch}
          classes={styles.btn}
          disabled={state.criteria === ''}
          title={state.criteria === '' ? 'Please, select the search criteria' : ''}
        />
      </div>

      <div className={styles['filter']}>
        <span>Search Criteria:</span>
        <select
          name='criteria'
          className={styles.criteria}
          value={state.criteria}
          onChange={changeHandler.bind(null, 'criteria')}
        >
          <option value={''}>Not selected</option>
          {criterion.map((c) => (
            <option key={c.value} value={c.value}>
              {c.text}
            </option>
          ))}
        </select>
      </div>
    </form>
  );
};

export default SearchBar;
