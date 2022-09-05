import { FC, memo, ReactElement, useCallback } from 'react';
import { useSearchParams } from 'react-router-dom';

import { faArrowDown, faArrowUp, faWarning } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

import { IUserBase } from '../shared/interfaces/user';
import LoadingSpinner from '../shared/LoadingSpinner';
import TableItem from './TableItem';

import styles from './Table.module.css';

interface ITableInterface<T> {
  title: string;
  headers: { [prop: string]: string };
  content: T[] | null;
  isLoading: boolean;
  errorMessage: string | null;
  defaultSort: string;
  defaultOrder: 'asc' | 'desc';
}

const Table: FC<ITableInterface<IUserBase>> = ({
  title,
  headers,
  content,
  isLoading,
  errorMessage,
  defaultSort,
  defaultOrder,
}) => {
  const [searchParams, setSearchParams] = useSearchParams();
  const sort = searchParams.get('sort') || defaultSort;
  const order = searchParams.get('order') || defaultOrder;
  const search = searchParams.get('search');

  let tableContent: ReactElement[] = [];
  let [loadingContent, errorContent, noContent]: JSX.Element[] = [];

  const onSortHandler = useCallback(
    (newSort: string) => {
      searchParams.set('sort', newSort);
      searchParams.set('order', order === 'asc' ? 'desc' : 'asc');
      setSearchParams(searchParams);
    },
    [searchParams, setSearchParams, order]
  );

  if (content?.length === 0 && !isLoading) {
    noContent = (
      <div className={styles['no-content']}>
        <FontAwesomeIcon icon={faWarning} className={styles.icon} />
        {search ? <h2>Sorry, we couldn't find what you're looking for.</h2> : <h2>There is no {title} yet.</h2>}
      </div>
    );
  }

  if (isLoading && !errorMessage) {
    loadingContent = (
      <div className={styles.loading}>
        <LoadingSpinner />
      </div>
    );
  }

  if (!isLoading && errorMessage) {
    errorContent = (
      <div className={styles.error}>
        <FontAwesomeIcon icon={faWarning} className={styles.icon} />
        <h2> {errorMessage}</h2>
      </div>
    );
  }

  if (content?.length && !isLoading && !errorMessage) {
    tableContent = content.map((currentUser) => (
      <TableItem headers={headers} currentUser={currentUser} key={currentUser._id}></TableItem>
    ));
  }

  return (
    <>
      <div className={styles['table-wrapper']}>
        {tableContent.length === 0 && (
          <div className={styles['loading-shade']}>
            {loadingContent && loadingContent}
            {errorContent && errorContent}
            {noContent && noContent}
          </div>
        )}
        <table className={styles.table}>
          <thead>
            <tr>
              {Object.entries(headers).map(([k, v]) => {
                if (k === 'action' || k.includes('image')) {
                  return <th key={k}>{v}</th>;
                } else {
                  return (
                    <th key={k} onClick={onSortHandler.bind(null, k)}>
                      {v}
                      <FontAwesomeIcon
                        icon={order === 'asc' ? faArrowDown : faArrowUp}
                        className={`${styles.icon} ${sort === k ? styles['active-icon'] : ''}`}
                      />
                    </th>
                  );
                }
              })}
            </tr>
          </thead>
          <tbody>{tableContent}</tbody>
        </table>
      </div>
    </>
  );
};

export default memo(Table);
