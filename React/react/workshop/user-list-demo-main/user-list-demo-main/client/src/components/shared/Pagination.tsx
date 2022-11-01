import { FC, FormEvent, useCallback, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';

import {
  faAngleLeft,
  faAngleRight,
  faAnglesLeft,
  faAnglesRight,
} from '@fortawesome/free-solid-svg-icons';

import Button from './Button';
import styles from './Pagination.module.css';

const Pagination: FC<{
  maxPages: number;
  pageSizeOptions: number[];
  showFirstLastButtons?: boolean;
  classes?: string;
  position?: 'left' | 'right' | 'center';
}> = ({ maxPages, pageSizeOptions, showFirstLastButtons, classes, position = 'right' }) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const pageIndex = Number(searchParams.get('page') || 1);
  const limit = searchParams.get('limit') || pageSizeOptions[0];

  useEffect(() => {
    searchParams.set('page', pageIndex.toString());
    searchParams.set('limit', limit.toString());
    setSearchParams(searchParams);
  }, [pageIndex, limit, searchParams, setSearchParams]);

  const setNewParams = useCallback(
    (destination: 'first' | 'previous' | 'next' | 'last') => {
      let currentPage = 1;
      if (destination === 'first') {
        searchParams.set('page', '1');
        setSearchParams(searchParams);
      } else if (destination === 'next') {
        currentPage = pageIndex + 1 >= maxPages ? maxPages : pageIndex + 1;
        searchParams.set('page', currentPage.toString());
        setSearchParams(searchParams);
      } else if (destination === 'previous') {
        currentPage = pageIndex - 1 < 1 ? 1 : pageIndex - 1;
        searchParams.set('page', currentPage.toString());
        setSearchParams(searchParams);
      } else {
        searchParams.set('page', maxPages.toString());
        setSearchParams(searchParams);
      }
    },
    [searchParams, setSearchParams, pageIndex, maxPages]
  );

  const changeLimitHandler = useCallback(
    ({ currentTarget: { value } }: FormEvent<HTMLSelectElement>) => {
      searchParams.set('limit', value);
      setSearchParams(searchParams);
    },
    [searchParams, setSearchParams]
  );

  return (
    <div className={`${styles.pagination} ${classes} ${styles[position]}`}>
      <div className={styles.limits}>
        <span>Items per page:</span>
        <select name='limit' className={styles.limit} value={limit} onChange={changeLimitHandler}>
          {pageSizeOptions.map((v) => (
            <option key={v} value={v}>
              {v}
            </option>
          ))}
        </select>
      </div>
      <p className={styles.pages}>
        {pageIndex} - {maxPages} of {maxPages}
      </p>
      <div className={styles.actions}>
        {showFirstLastButtons && (
          <Button
            classes={styles.btn}
            icon={faAnglesLeft}
            onClick={setNewParams.bind(null, 'first')}
            disabled={pageIndex === 1}
            title='First Page'
          ></Button>
        )}
        <Button
          classes={styles.btn}
          icon={faAngleLeft}
          onClick={setNewParams.bind(null, 'previous')}
          disabled={pageIndex === 1}
          title='Previous Page'
        ></Button>
        <Button
          classes={styles.btn}
          onClick={setNewParams.bind(null, 'next')}
          disabled={pageIndex >= maxPages}
          icon={faAngleRight}
          title='Next Page'
        ></Button>
        {showFirstLastButtons && (
          <Button
            classes={styles.btn}
            onClick={setNewParams.bind(null, 'last')}
            disabled={pageIndex >= maxPages}
            icon={faAnglesRight}
            title='Last Page'
          ></Button>
        )}
      </div>
    </div>
  );
};

export default Pagination;
