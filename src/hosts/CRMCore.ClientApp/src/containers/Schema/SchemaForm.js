import * as React from 'react';
import { connect } from 'react-redux';
import { reduxForm } from 'redux-form';

import {
  Card,
  CardHeader,
  CardBody,
  Form,
  FormGroup,
  Label,
  Button
} from 'reactstrap';

import * as SchemaStore from 'redux/modules/schema';

class SchemaForm extends React.Component {
  componentDidMount() {
    const { match } = this.props;
    console.log(match);
    if (match.params && match.params.name) {
      this.props.loadSpecificSchema(match.params.name);
    }
  }

  render() {
    const { match } = this.props;
    return <Form>{match.params.name}</Form>;
  }
}

const validate = values => {
  const errors = {};
  return errors;
};

const initData = state => ({
  initialValues: state.schema.schemaSelected || {}
});

export default connect(initData, SchemaStore.actionCreators)(
  reduxForm({
    form: 'schemaForm',
    enableReinitialize: true,
    validate,
    onSubmit: (values, dispatch, props) => {
      const { match } = props;
      if (match.params && !match.params.id) {
        // props.addSchema(values);
      } else {
        // props.updateSchema(values);
      }
    }
  })(SchemaForm)
);
