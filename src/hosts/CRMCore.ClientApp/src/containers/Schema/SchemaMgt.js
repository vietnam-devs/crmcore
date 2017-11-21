import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { Card, CardHeader, CardBody, Button } from 'reactstrap';

import { actionCreators as schemaActionCreators } from 'redux/modules/schema';
import SchemaTable from './SchemaTable';

class SchemaMgt extends Component {
  constructor(props) {
    super(props);
    this.addRow = this.addRow.bind(this);
  }

  addRow() {
    this.props.history.replace('/metadata/schemas/add');
  }

  render() {
    console.log(this.props);
    /*const schemas = this.props.schemas.map((id, idx) => {
      return this.props.fieldsById[id];
    });*/

    var table = (
      <SchemaTable
        totalPages={this.props.totalPages || 1}
        loading={this.props.loading}
        schemas={this.props.schemas}
        loadSchemas={this.props.loadSchemas}
        history={this.props.history}
      />
    );

    return (
      <div className="animated fadeIn">
        <Card className="b-panel">
          <CardHeader>
            <h3 className="b-panel-title">
              <i className="icon-notebook b-icon" />
              Schema Page
            </h3>
            <span className="b-panel-actions">
              <Button
                color="success"
                className="pull-right"
                onClick={this.addRow}
              >
                <i className="icon-plus b-icon" />Add
              </Button>
            </span>
          </CardHeader>
          <CardBody className="card-body">{table}</CardBody>
        </Card>
      </div>
    );
  }
}

function mapStateToProps(state, ownProps) {
  return {
    ...state.schema
  };
}

export const mapDispatchToProps = dispatch =>
  bindActionCreators(schemaActionCreators, dispatch);

export default connect(mapStateToProps, mapDispatchToProps)(SchemaMgt);
