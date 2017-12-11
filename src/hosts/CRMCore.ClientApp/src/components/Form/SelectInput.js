import * as React from 'react';
import { Async } from 'react-select';
import { get } from 'redux/client';

import 'react-select/dist/react-select.css';

export const renderSelect = ({ input, meta, uri }) => {
  const getOptions = (input, callback) => {
    get(uri)
      .map(ajaxResponse => {
        return ajaxResponse.response;
      })
      .map(response => {
        return response.map(item => {
          return {
            value: item.key,
            label: item.value
          };
        });
      })
      .subscribe(json => {
        setTimeout(function() {
          callback(null, { options: json, complete: true });
        }, 30);
      });
  };

  return (
    <div>
      <Async
        name={input.name}
        value={input.value}
        onChange={input.onChange}
        loadOptions={getOptions}
        onBlur={() => input.onBlur(input.value)}
      />
    </div>
  );
};
