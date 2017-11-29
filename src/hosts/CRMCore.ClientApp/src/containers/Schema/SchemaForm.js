import * as React from 'react';
import { connect } from 'react-redux';
import { Field, reduxForm } from 'redux-form';

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
import { TextBoxField, NumberField, CheckboxField } from 'components';

class SchemaForm extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      canDelete: false,
      showConfirm: false
    };
    this.deleteBlog = this.deleteBlog.bind(this);
    this.onConfirmationCancel = this.onConfirmationCancel.bind(this);
  }

  deleteBlog(e) {
    e.preventDefault();
  }

  onConfirmationCancel(showConfirm) {
    this.setState({ showConfirm: showConfirm });
  }

  render() {
    const { error, handleSubmit, pristine, reset, submitting } = this.props;
    return (
      <div className="animated fadeIn">
        <Card className="b-panel">
          <CardBody className="card-body">
            <Form className="b-form">
              <FormGroup>
                <Label for="name">Name</Label>
                <Field
                  name="name"
                  placeholder="Name"
                  type="text"
                  component={TextBoxField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="properties.label">Label (props)</Label>
                <Field
                  name="properties.label"
                  placeholder="Label"
                  type="text"
                  component={TextBoxField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="properties.hints">Hints (props)</Label>
                <Field
                  name="properties.hints"
                  placeholder="Hints"
                  type="text"
                  component={TextBoxField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="isPublished">Published?</Label>
                <Field
                  name="isPublished"
                  placeholder="Published"
                  type="checkbox"
                  component={CheckboxField}
                />
              </FormGroup>
              <FormGroup>
                <Label for="version">Version</Label>
                <Field
                  name="version"
                  placeholder="Version"
                  type="number"
                  component={NumberField}
                />
              </FormGroup>
              <FormGroup>
                <Button
                  color="primary"
                  type="submit"
                  disabled={pristine || submitting}
                >
                  <i className="icon-paper-plane b-icon" />Save
                </Button>&nbsp;
                <Button
                  color="warning"
                  disabled={pristine || submitting}
                  onClick={reset}
                >
                  <i className="icon-reload b-icon" />Reset
                </Button>&nbsp;
                <Button
                  color="danger"
                  disabled={!this.state.canDelete}
                  onClick={() => {
                    this.setState({
                      showConfirm: !this.state.showConfirm
                    });
                  }}
                >
                  <i className="icon-trash b-icon" />Delete
                </Button>
              </FormGroup>
            </Form>
          </CardBody>
        </Card>
      </div>
    );
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
      // if (match.params && !match.params.id) {
        // props.addSchema(values);
      // } else {
        // props.updateSchema(values);
      // }
    }
  })(SchemaForm)
);
