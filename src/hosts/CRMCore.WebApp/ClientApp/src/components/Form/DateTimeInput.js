import 'flatpickr/dist/flatpickr.min.css';
import 'flatpickr/dist/themes/airbnb.css';

import React, { PureComponent } from 'react';
import { FormFeedback } from 'reactstrap';
import Flatpickr from 'flatpickr';

export default class DateTimeInput extends PureComponent {
  componentDidMount() {
    this.flatpickr = new Flatpickr(this.node, {
      dateFormat: this.props.formatDateTime
    });
  }

  componentWillUnmount() {
    this.flatpickr.destroy();
  }

  render() {
    const {
      meta: { touched, error, warning },
      input: { value },
      input,
      label
    } = this.props;
    
    return (
      <div>
        <input
          type={'input'}
          className="form-control"
          placeholder={label}
          value={value}
          ref={node => {
            this.node = node;
          }}
          data-enable-time
          {...this.props.input}
        />
        {touched &&
          ((error && (
            <FormFeedback className="text-error b-form-feedback">
              {error}
            </FormFeedback>
          )) ||
            (warning && (
              <FormFeedback className="b-form-feedback">{warning}</FormFeedback>
            )))}
      </div>
    );
  }
}
