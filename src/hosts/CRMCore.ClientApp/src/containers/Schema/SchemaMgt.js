import React, { Component } from 'react';
import { bindActionCreators } from 'redux';
import { connect } from 'react-redux';

import { Row, Col, Card, CardHeader, CardBody, Button } from 'reactstrap';

import { actionCreators as schemaActionCreators } from 'redux/modules/schema';
import SchemaTable from './SchemaTable';
import SchemaForm from './SchemaForm';

class SchemaMgt extends Component {
  constructor(props) {
    super(props);
    this.addRow = this.addRow.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  componentDidMount() {
    this.props.loadSchemas(this.props.page + 1);
  }

  addRow() {
    this.props.history.push('/metadata/schema-form/$');
  }

  handleClick(name) {
    console.log(name);
    this.props.loadSpecificSchema(name);
  }

  render() {
    /*var table = (
      <SchemaTable
        totalPages={this.props.totalPages || 1}
        loading={this.props.loading}
        schemas={this.props.schemas}
        loadSchemas={this.props.loadSchemas}
        history={this.props.history}
      />
    );*/

    console.log(this.props);
    const { schemaByIds, schemas, schemaSelected } = this.props;

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
          <CardBody className="card-body">
            {/*table*/}
            <Row>
              <Col>
                <div className="list-group">
                  {schemaByIds.map(id => (
                    <a
                      key={id}
                      className="list-group-item list-group-item-action flex-column align-items-start"
                      onClick={() => this.handleClick(schemas[id].name)}
                    >
                      <div className="d-flex w-100 justify-content-between">
                        <h5 className="mb-1">{schemas[id].name}</h5>
                        <small>
                          {schemas[id].isPublished ? 'Published' : 'Draft'}
                        </small>
                      </div>
                      <p className="mb-1">{schemas[id].properties.label}</p>
                      <small>{schemas[id].properties.hints}</small>
                    </a>
                  ))}
                </div>
              </Col>
              <Col>
                <SchemaForm />
              </Col>
            </Row>
          </CardBody>
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
